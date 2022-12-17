
namespace UnchartedWaters_CSHARP {//begin namespace
    internal class Program {//begin class
        static readonly int[,,] data = GetSatelliteData();
        static int totalNavy = 0;
        static int totalEnemy = 0;
        static int totalEmpty = 0;
        static int[] navyPerLevel = new int[3];
        static int[] enemyPerLevel = new int[3];
        static int[] emptyPerLevel = new int[3];

        static void Main(string[] args) {//begin main

            //CREATE ARRAYS
            int[] navyPerLevel = new int[3];
            int[] enemyPerLevel = new int[3];
            int[] emptyPerLevel = new int[3];

            //SET CONSOLE COLORS
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            //POPULATE ARRAYS WITH PER LEVEL TOTALS AND TOTAL COUNTS
            GetCountTotals();

            //CALCULATE TOTAL RATIO OF FRIENDLY TO ENEMYS SUBS
            double totalFriendlyToEnemyRatio = (double)totalNavy / (double)totalEnemy;

            //POPULATE GO FOR ATTACK AKA attackperlevel[]
            bool[] attackperlevel = GetGoForAttack();

            //OUTPUT
            Console.WriteLine("              UNCHARTED WATERS                   ");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Surface sub density:        {GetSubDensityPerLevel(0)} ");
            Console.WriteLine($"Underwater sub density:     {GetSubDensityPerLevel(1)} ");
            Console.WriteLine($"Deepwater sub density:      {GetSubDensityPerLevel(2)} ");
            Console.WriteLine($"Sub / Enemy sub ratio:      {totalFriendlyToEnemyRatio} ");
            Console.WriteLine($"Go for surface attack:      {attackperlevel[0]} ");
            Console.WriteLine($"Go for underwater attack:   {attackperlevel[1]} ");
            Console.WriteLine($"Go for deepwater attack:    {attackperlevel[2]} ");
            //Console.WriteLine(" ");

            // PRINT'UM
            PrintArrays();

            Console.ReadLine();

        }//end main

        //PRINT ARRAY FUNCTION
        static void PrintArrays() {

            // CHANGE TXT COLOR FOR EACH LEVEL OF DATA
            for (int level = 0; level < data.GetLength(2); level++) {
                // IF SURFACE
                if (level == 0) {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n");
                    Console.WriteLine("SURFACE");
                    
                    // IF UNDERWATER
                } else if (level == 1) {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("UNDER WATER");
                    
                    // IF DEEPWATER
                } else if (level == 2) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("DEEP WATER");
                }
                //FOR EACH COLUMN/ROW
                for (int column = 0; column < data.GetLength(1); column++) {
                    for (int x = 0; x < data.GetLength(0); x++) {
                        //PRINT ELEMENT PLUS SPACE
                        Console.Write(data[x, column, level] + " ");
                    }
                    Console.WriteLine();
                }//END INNER FOR
            }//END OUTER FOR
        }//END PRINT ARRAYS

        //GO FOR ATTACK FUNCTION
        static bool[] GetGoForAttack() {
            
            //CREATE BOOL[]
            bool[] attackperlevel = new bool[3];

            //POPULATE BOOL[] FOR EACH LEVEL
            for (int i = 0; i < 3; i++) {
                
                //IF RATIO FAVORS NAVY
                if (GetFriendlyToEnemyRatioPerLevel(i) > 1) {
                    attackperlevel[i] = true;
                }//END IF
            }//END FOR

            return attackperlevel;

        }//END GETGOFORATTACK

        //CALCULATE DENSITY FUNCTION
        static double GetSubDensityPerLevel(int level) {
            double total = 0.0;
            int totalNumberOfElements = 0;
            int totalNumberOfSubs = 0;

            //SUM TOTAL OF ALL ELEMENTS(EQUAL TO LENGTH OF 3DARRAY
            totalNumberOfElements = (navyPerLevel[level] + enemyPerLevel[level] + emptyPerLevel[level]);

            //TOTAL NUMBER OF SUBS
            totalNumberOfSubs = navyPerLevel[level] + enemyPerLevel[level];

            //CALCULATE RATIO
            total = totalNumberOfSubs / (double)totalNumberOfElements;

            return total;
        }

        //CALCULATE FRIENDLY TO ENEMY RATIO PER LEVEL -- (used in GetGoForAttack function)
        static double GetFriendlyToEnemyRatioPerLevel(int level) {
            double ratio = navyPerLevel[level] / (double)enemyPerLevel[level];
            return ratio;
        }

        //CALCULATE COUNT TOTALS FUNCTION- TOTAL AND PER LEVEL
        static void GetCountTotals() {

            int perlevelNavy = 0;
            int perlevelEnemy = 0;
            int perlevelEmpty = 0;

            //FOR EACH LEVEL
            for (int level = 0; level < data.GetLength(2); level++) {
                // COLUMN
                for (int column = 0; column < data.GetLength(1); column++) {
                    // ROW
                    for (int row = 0; row < data.GetLength(0); row++) {

                        //IF NAVY
                        // COUNT NAVY, PER LEVEL & TOTAL
                        if (data[row, column, level] == 1) {
                            totalNavy++;
                            perlevelNavy++;
                        };
                        //IF ENEMY
                        // COUNT ENEMY, PER LEVEL & TOTAL
                        if (data[row, column, level] == 2) {
                            totalEnemy++;
                            perlevelEnemy++;
                        };
                        //IF EMPTY
                        // COUNT EMPTY, PER LEVEL & TOTAL
                        if (data[row, column, level] == 0) {
                            totalEmpty++;
                            perlevelEmpty++;
                        };

                    }//END LEVEL3 FOR
                }//END LEVEL 2 FOR

                // POPULATE TOTALS
                navyPerLevel[level] = perlevelNavy;
                enemyPerLevel[level] = perlevelEnemy;
                emptyPerLevel[level] = perlevelEmpty;

                // RESET TOTALS
                perlevelNavy = 0;
                perlevelEnemy = 0;
                perlevelEmpty = 0;
            
            }//END TOP LEVEL(1) FOR

        }//END GETCOUNTTOTALS
        static int[,,] GetSatelliteData() {
            Random rand = new Random();
            int[,,] data = new int[10, 10, 3];

            for (int level = 0; level < data.GetLength(2); level++) {

                for (int column = 0; column < data.GetLength(1); column++) {

                    for (int row = 0; row < data.GetLength(0); row++) {

                        if (rand.Next(0, 101) < 25) {
                            data[row, column, level] = rand.Next(1, 3);
                        }
                    }
                }
            }
            return data;
        }//END GET SATELLITE DATA

    }//end class
}//end namespace

