using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Poi.Hrm.Logic.Interface;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class FaceRecognitionService : IFaceRecognitionService
{
    private readonly LBPHFaceRecognizer _faceRecognizer;
    private readonly Dictionary<int, string> _labels;
    private readonly string _modelPath = Path.Combine("wwwroot", "face_model.xml");
    private readonly ILogger<FaceRecognitionService> _logger;
    private readonly CascadeClassifier _faceCascade;
    private readonly HrmDbContext _context;

    public FaceRecognitionService(ILogger<FaceRecognitionService> logger, HrmDbContext context)
    {
        _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 123.0); // Parameters can be tuned
        _labels = new Dictionary<int, string>();
        _logger = logger;
        _faceCascade = new CascadeClassifier(Path.Combine("wwwroot", "haarcascade_frontalface_default.xml")); // Path to Haar Cascade XML file
        _context = context;

        if (File.Exists(_modelPath))
        {
            _faceRecognizer.Read(_modelPath);
            LoadLabels();
        }
    }

    public async Task TrainAsync(string datasetDir)
    {
        var images = new List<Image<Gray, byte>>();
        var labels = new List<int>();
        int labelIndex = 0;

        foreach (var personDir in Directory.GetDirectories(datasetDir))
        {
            var personName = Path.GetFileName(personDir);
            foreach (var imagePath in Directory.GetFiles(personDir, "*.jpg"))
            {
                var image = new Image<Gray, byte>(imagePath);
                var preprocessedImage = PreprocessImage(image);
                if (preprocessedImage != null)
                {
                    images.Add(preprocessedImage);
                    labels.Add(labelIndex);
                }
            }

            _labels[labelIndex] = personName;
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
        if (image == null)
        {
            _logger.LogInformation("No face detected in the input image.");
            return "Unknown";
        }

        var result = await Task.Run(() => _faceRecognizer.Predict(image));

        if (result.Distance < 110 &&  result.Label != -1 && _labels.TryGetValue(result.Label, out var name))
        {
            _logger.LogInformation($"Face recognized: {name}");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == info.UserId);

            // do check later
            if (user.UserName != name)
            {
                return "Unknown";
            }

            return user.FullName;
        }

        _logger.LogInformation("Face not recognized.");
        return "Unknown";
    }

    private Image<Gray, byte> PreprocessImage(Image<Gray, byte> image)
    {
        // Detect faces in the image
        var faces = _faceCascade.DetectMultiScale(image, 1.1, 5, new Size(30, 30), Size.Empty);

        if (faces.Length == 0)
        {
            // No faces detected, return null
            return null;
        }

        // Use the first detected face
        var faceRect = faces[0];
        var faceImage = image.GetSubRect(faceRect).Clone();

        // Resize the face image to a standard size (e.g., 100x100) and apply histogram equalization
        faceImage = faceImage.Resize(100, 100, Inter.Linear);
        faceImage._EqualizeHist();

        return faceImage;
    }

    private void SaveLabels()
    {
        using (var writer = new StreamWriter(Path.Combine("wwwroot", "labels.txt")))
        {
            foreach (var label in _labels)
            {
                writer.WriteLine($"{label.Key},{label.Value}");
            }
        }
    }

    private void LoadLabels()
    {
        foreach (var line in File.ReadLines(Path.Combine("wwwroot", "labels.txt")))
        {
            var parts = line.Split(',');
            _labels[int.Parse(parts[0])] = parts[1];
        }
    }
}
