namespace AdventOfCode.Core.Puzzles.Day06;

public class Solution 
{
    public object SolvePart1(int[] state, int numberOfDays)
    {
        for (var day = 0; day < numberOfDays; day++)
        {
            List<int> newState = new List<int>();
            int livesToAdd = 0;
            foreach (var item in state)
            {
                if (item > 0)
                    newState.Add(item - 1);
                else
                {
                    newState.Add(6);
                    livesToAdd += 1;
                }
            }

            for (int newLives = 0; newLives < livesToAdd; newLives++)
                newState.Add(8);

            state = newState.ToArray();
        }

        return state;
    }

    public object SolvePart2(int[] state, int numberOfDays)
    {
        Dictionary<int, long> newState = new();

        for (var i = 0; i <= 8; i++) 
            newState[i] = state.Count(x => x == i);

        for (var i = 0; i < 256; i++)
        {
            var breeders = newState[0];

            for (var j = 0; j < 8; j++) 
                newState[j] = newState[j + 1];
            
            newState[6] += breeders;
            newState[8] = breeders;
        }
        
        return newState.Values.Sum();
    }
}