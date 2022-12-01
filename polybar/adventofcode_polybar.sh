cookie=$(head -n 1 $HOME/.aoc_cookie.txt)
curl -s https://adventofcode.com/2022/leaderboard/private/view/1538851.json -X GET -H "Cookie: session=$cookie" > /tmp/aoc.json
jq '[.members[] | { position: .key, score: .local_score, name: .name }]|sort_by(.score)|reverse|sort_by(.last_star_ts)|to_entries|.[] | select(.value.name=="Jacob Duijzer")|"AoC: position: \(.key+1), score: \(.value.score)"' /tmp/aoc.json | sed 's/\"//g'
