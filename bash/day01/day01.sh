cat $1 | awk 'BEGIN {if(NR == 1) depth=$1} {if(NR > 1) {if($1 > depth) ++count} {depth=$1} }END {print count}'
