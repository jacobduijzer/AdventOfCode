#!/usr/bin/env bash

cat $1 | awk '
BEGIN { FS = "" }  
{
  digit1 = digit2 = ""
  for (i = 1; i <= NF; i++)
    if ($i ~ /[1-9]/) {
      if (!digit1) digit1 = $i
      digit2 = $i
    }
    t += digit1 * 10 + digit2
}
END { print t }'
