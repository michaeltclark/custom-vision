using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;

using System.Drawing;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.Util;

System.Console.WriteLine("Welcome!");

var filename = "webcam.jpg";
using var capture = new VideoCapture(0, VideoCapture.API.Any);

// Required to initialize the Sony Webcam Utility. 
capture.Start();
Thread.Sleep(4000);

// Takes picture
var image = capture.QueryFrame(); 
image.Save(filename);


// Configuration Management Refactor into its own class(es)
var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true);

var config = builder.Build(); 

var client = AzureCVQuickstart.ReturnPredictionClient(config["PredictionEndpoint"],config["PredictionKey"]);

using (var _stream = new FileStream(filename, FileMode.Open))
{
    var detectionResult = client.DetectImage(new Guid("d6982aaa-41cc-4db5-9078-5e6dfde446c1"), "Iteration1", _stream);
    var _boundingBox = detectionResult.Predictions.First().BoundingBox;

    System.Console.WriteLine($"Left: {_boundingBox.Left} Top: {_boundingBox.Top}");
    // Box Left is prediction left * Image Width
    System.Console.WriteLine($"Left: {_boundingBox.Left * image.Width} Top: {_boundingBox.Top * image.Height}");
    
    // Refactor this into a rectangle creator class? 
    CvInvoke.Rectangle(image, new Rectangle(new Point(Convert.ToInt32(_boundingBox.Left * image.Width), Convert.ToInt32(_boundingBox.Top * image.Height)), new Size(Convert.ToInt32(_boundingBox.Width * image.Width), Convert.ToInt32(_boundingBox.Height * image.Height))), new MCvScalar(120, 120, 120));

    image.Save(filename);

}

// Draw the "line" rectangle across the screen vertically. Give it a Red color. 

// Sample an image from we webcam ever so often

// Send request.

// Determine if the X of the image became > or < the X of the "line" rectangle between samples.  