using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


using Emgu.CV;


public static class AzureCVQuickstart
{

    private static List<string> hemlockImages;
    private static List<string> japaneseCherryImages;
    private static Tag hemlockTag;
    private static Tag japaneseCherryTag;
    private static Iteration iteration;
    private static string publishedModelName = "";
    private static MemoryStream testImage;


    // static CustomVisionTrainingClient trainingApi = AuthenticateTraining(trainingEndpoint, trainingKey);
    // static CustomVisionPredictionClient predictionApi = AuthenticatePrediction(predictionEndpoint, predictionKey);

    // Project project = CreateProject(trainingApi);
    // AddTags(trainingApi, project);
    // UploadImages(trainingApi, project);
    // TrainProject(trainingApi, project);
    // PublishIteration(trainingApi, project);
    // TestIteration(predictionApi, project);
    // DeleteProject(trainingApi, project);

    private static CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
    {
        // Create the Api, passing in the training key
        CustomVisionTrainingClient trainingApi = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
        {
            Endpoint = endpoint
        };
        return trainingApi;
    }
    private static CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
    {
        // Create a prediction endpoint, passing in the obtained prediction key
        CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
        {
            Endpoint = endpoint
        };
        return predictionApi;
    }

    public static CustomVisionPredictionClient ReturnPredictionClient(string predictionEndpoint, string predictionKey)
    {
        return AuthenticatePrediction(predictionEndpoint, predictionKey);
    }
}