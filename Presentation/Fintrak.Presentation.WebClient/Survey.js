


$scope.SurveyFormData = {
    Survey: {},
    Questions: []
};



$scope.SurveyFormData.Survey = { Name: 'Survey 1', Description: 'Survey 1' };
$scope.SurveyFormData.Questions = [
        {
            Text: 'Are you a smoker', OptionType: 'trueOrFalse',
            Options: ['Yes', 'No']
        },
         {
             Text: 'What is your name', OprtionType: 'SigleAnswer',
             Options: []
         },
         {
             Text: 'What brand did you prefer', OprtionType: 'MultipleChoice',
             Options: ['Brand 1', 'Brand 2', 'Brand 3']
         }
];

$scope.currentQuestion = null;
$scope.currentOptionType = null;
$scope.currentOption = null;
$scope.currentQuestionOptions = [];

$scope.AddQuestionOption = function (option) {

}

$scope.AddQuestion = function () {
    $scope.SurveyFormData.Questions.push({
        Text: $scope.currentQuestion, OprtionType: $scope.currentOptionType,
        Options: $scope.currentQuestionOptions
    });
}

