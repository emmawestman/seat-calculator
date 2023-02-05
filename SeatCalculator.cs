using System.Text.RegularExpressions;
using MoreLinq;

public class SeatCalculator {

        public int GetOptimalHappinesScore(string inputPath) {
            var fileLines = readFile(inputPath);
            RulesAndGuest  rulesAndGuest = generateRulesAndGuests(fileLines);
            
            return calculateHappiestScore(rulesAndGuest.Guests, rulesAndGuest.Rules);
        } 

        public int GetOptimalHappinesScoreWithMeAsGuest(string inputPath, string myName) {
            var fileLines = readFile(inputPath);
            RulesAndGuest  rulesAndGuest = generateRulesAndGuests(fileLines);
            Dictionary<string, int> rulesWithMe = rulesAndGuest.Rules;

            foreach(var guest in rulesAndGuest.Guests) {
                rulesWithMe.Add($"{myName}-{guest}", 0);
                rulesWithMe.Add($"{guest}-{myName}", 0);
            }

            List<string> guestsWithMe = rulesAndGuest.Guests;
            guestsWithMe.Add(myName);
            
            return calculateHappiestScore(guestsWithMe, rulesWithMe);
        } 
        
        private string[] readFile (string path) {
            string[] readText = File.ReadAllLines(path);

            return readText;
        }


        private RulesAndGuest generateRulesAndGuests (string[] rules) {
           Dictionary<string, int> ruleSet = new Dictionary<string, int> {};
           List<string> guests = new List<string>();

            foreach(string row in rules) {

                Regex re = new Regex(@"(\w+) would (lose|gain) (\d+) happiness units by sitting next to (\w+)\.");
                Match match = re.Match(row);

                var dinnerGuestA = match.Groups[1].Value;
                var dinnerGuestB = match.Groups[4].Value;

                int points = Int32.Parse(match.Groups[3].Value);
                var gainOrLose = match.Groups[2].Value;

                var happinesPoints = gainOrLose.Equals("gain") ? points : -1 *points;
            
                guests.Add(dinnerGuestA);
                ruleSet.Add($"{dinnerGuestA}-{dinnerGuestB}", happinesPoints);
            }

            var uniqueGuests = guests.Distinct().ToList();

            RulesAndGuest rulesAndGuest = new RulesAndGuest {
                Guests = uniqueGuests,
                Rules = ruleSet
            };

            return rulesAndGuest;
        }
 
        private int calculateHappiestScore (List<string> guests, Dictionary<string, int> rules) {
            var permutations = guests.Permutations();
            List<int> seatingScores = new List<int>();

            foreach(var potentialSeating in permutations) {
                List<string> seatingOrder =  potentialSeating.ToList();
                seatingOrder.Add(potentialSeating.First());
                
                int score = 0;
             
                for(int i = 0; i < seatingOrder.Count()-1; i++) {
                    var key = $"{seatingOrder[i]}-{seatingOrder[i+1]}";
                    var key2 = $"{seatingOrder[i+1]}-{seatingOrder[i]}";
                    int pairScore = rules[key] + rules[key2];
                    score += pairScore;
                }

                seatingScores.Add(score);
            }
            seatingScores.Sort();

            return seatingScores.Last(); 
        }
    }
