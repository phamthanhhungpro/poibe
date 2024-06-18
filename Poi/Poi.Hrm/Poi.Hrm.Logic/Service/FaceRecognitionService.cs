using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Poi.Hrm.Logic.Interface;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using System.Drawing;

public class FaceRecognitionService : IFaceRecognitionService
{
    private readonly LBPHFaceRecognizer _faceRecognizer;
    private readonly Dictionary<int, string> _labels;
    private readonly string _modelPath;
    private readonly ILogger<FaceRecognitionService> _logger;
    private readonly CascadeClassifier _faceCascade;
    private readonly HrmDbContext _context;

    public FaceRecognitionService(ILogger<FaceRecognitionService> logger, HrmDbContext context)
    {
        _modelPath = Path.Combine("wwwroot", "face_model.xml");
        _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 90); // Tuned parameter
        _labels = new Dictionary<int, string>();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _faceCascade = new CascadeClassifier(Path.Combine("wwwroot", "haarcascade_frontalface_default.xml"));

        if (File.Exists(_modelPath))
        {
            _faceRecognizer.Read(_modelPath);
            LoadLabels();
        }
    }

    public async Task TrainAsync(string datasetDir)
    {
        if (!Directory.Exists(datasetDir))
        {
            throw new DirectoryNotFoundException($"The dataset directory {datasetDir} does not exist.");
        }

        var images = new List<Image<Gray, byte>>();
        var labels = new List<int>();
        int labelIndex = 0;

        var directories = Directory.GetDirectories(datasetDir);
        foreach (var personDir in directories)
        {
            var personName = Path.GetFileName(personDir);
            var imagePaths = Directory.EnumerateFiles(personDir, "*.*")
                                      .Where(file => file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".jpeg"));

            foreach (var imagePath in imagePaths)
            {
                try
                {
                    var image = new Image<Gray, byte>(imagePath);
                    var preprocessedImage = PreprocessImage(image);
                    if (preprocessedImage != null)
                    {
                        lock (images)
                        {
                            images.Add(preprocessedImage);
                            labels.Add(labelIndex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing image {imagePath}");
                }
            }

            lock (_labels)
            {
                _labels[labelIndex] = personName;
            }
            labelIndex++;
        }

        var faceImagesArray = new VectorOfMat(images.Select(img => img.Mat).ToArray());
        var faceLabelsArray = new VectorOfInt(labels.ToArray());

        await Task.Run(() =>
        {
            _logger.LogInformation("Training model...");
            _faceRecognizer.Train(faceImagesArray, faceLabelsArray);
            _faceRecognizer.Write(_modelPath);
            SaveLabels();
            _logger.LogInformation("Model trained and saved.");
        });
    }

    public async Task<string> RecognizeAsync(byte[] imageBytes, TenantInfo info)
    {
        var imageMat = new Mat();
        CvInvoke.Imdecode(imageBytes, ImreadModes.Grayscale, imageMat);

        var image = PreprocessImage(imageMat.ToImage<Gray, byte>());

        // Save the image to a file for manual checking
        var outputPath = Path.Combine("wwwroot", "snapshot", $"{Guid.NewGuid()}.jpg");
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

        image?.Save(outputPath);
        if (image == null)
        {
            _logger.LogInformation("No face detected in the input image.");
            return "Unknown";
        }

        var result = await Task.Run(() => _faceRecognizer.Predict(image));

        if (result.Distance < 90 && result.Label != -1 && _labels.TryGetValue(result.Label, out var name))
        {
            _logger.LogInformation($"Face recognized: {name}");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
            var checkIn = new HrmDiemDanhHistory
            {
                Time = DateTime.UtcNow,
                SnapShotPath = outputPath,
                User = user,
            };

            _context.HrmDiemDanhHistory.Add(checkIn);
            await _context.SaveChangesAsync();

            return user.FullName;
        }

        _logger.LogInformation("Face not recognized.");
        return "Unknown";
    }

    private Image<Gray, byte> PreprocessImage(Image<Gray, byte> image)
    {
        var faces = _faceCascade.DetectMultiScale(image, 1.1, 5, new Size(30, 30), Size.Empty);

        if (faces.Length == 0)
        {
            return null;
        }

        var largestFace = faces.OrderByDescending(f => f.Width * f.Height).FirstOrDefault();

        if (largestFace == default(Rectangle))
        {
            return null;
        }

        var faceImage = image.GetSubRect(largestFace).Clone();

        CvInvoke.EqualizeHist(faceImage, faceImage);

        faceImage = ApplyPreprocessingSteps(faceImage);

        faceImage = faceImage.Resize(100, 100, Inter.Linear);
        faceImage._EqualizeHist();

        return faceImage;
    }

    private Image<Gray, byte> ApplyPreprocessingSteps(Image<Gray, byte> faceImage)
    {
        faceImage = faceImage.SmoothGaussian(3);
        faceImage._EqualizeHist();

        return faceImage;
    }

    private void SaveLabels()
    {
        try
        {
            using (var writer = new StreamWriter(Path.Combine("wwwroot", "labels.txt")))
            {
                foreach (var label in _labels)
                {
                    writer.WriteLine($"{label.Key},{label.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving labels");
        }
    }

    private void LoadLabels()
    {
        try
        {
            foreach (var line in File.ReadLines(Path.Combine("wwwroot", "labels.txt")))
            {
                var parts = line.Split(',');
                _labels[int.Parse(parts[0])] = parts[1];
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading labels");
        }
    }
}
