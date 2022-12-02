awk '{ if($1==""){print sum; sum=0}else{ sum += $1} } END { print sum }' $1 | sort -n | tail -1
