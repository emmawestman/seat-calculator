SeatCalculator seatCalculator = new SeatCalculator();

var path = "input1.txt";
var path2 = "input2.txt";

var result = seatCalculator.GetOptimalHappinesScore(path);
var result2 = seatCalculator.GetOptimalHappinesScore(path2);
var result3 = seatCalculator.GetOptimalHappinesScoreWithMeAsGuest(path2, "Emma");

Console.WriteLine("Optimal Score is {0} with input file {1}", result, path);
Console.WriteLine("Optimal Score is {0} with input file {1}", result2, path2);
Console.WriteLine("Optimal Score is {0} with input file {1} and me as guest", result3, path2);


