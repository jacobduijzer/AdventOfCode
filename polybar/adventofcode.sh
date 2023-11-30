# Script to get your position from a Advent Of Code leaderboard

year=2023

usage () {
  echo
  echo "Not all parameters are specified."
  echo
  echo "Please specify the following parameters:"
  echo "-c cookie file: the location of the file with cookie information."
  echo "-i number: the id of the leaderboard."
  echo "-l board name: what name to show as the leaderboard name."
  echo "-n name: the name of the player you want to show the score for."
}

while getopts c:i:l:n: flag
do
  case "${flag}" in 
    c) cookie_file=${OPTARG};;
    i) board_id=${OPTARG};;
    l) leader_board_name=${OPTARG};;
    n) name=${OPTARG};;
  esac
done

[[ -z "$cookie_file" || -z "$board_id" || -z "$leader_board_name" || -z "$name" ]] && { usage ; exit 1; }

# Read cookie contents
cookie=$(head -n 1 $cookie_file)

# Get leaderboard json
curl -s https://adventofcode.com/$year/leaderboard/private/view/$board_id.json -X GET -H "Cookie: session=$cookie" > /tmp/aoc-$board_id.json

# Getting total number of players, players position and score.
# Output will look like this: 5, 1, 100 (total, position, score)
score=$(jq '( .members | length ), ( [.members[] | { position: .key, score: .local_score, name: .name }]|sort_by(.score)|reverse|sort_by(.last_star_ts)|to_entries|.[] | select(.value.name=="Jacob Duijzer")|"\(.key+1),\(.value.score)")' /tmp/aoc-$board_id.json | paste -sd, - | sed 's/\"//g')

# Parse results
results=(${score//,/ })
 
echo -n "$leader_board_name: position: ${results[1]}/${results[0]}, score: ${results[2]}"
