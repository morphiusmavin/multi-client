make -f ts4600_150.mak clean
make -f ts4600_150.mak &> out.txt

if grep -q error out.txt
 then
  find2 error out.txt
  exit 1
fi

if grep -q undefined out.txt
 then
  find2 undefined out.txt
  exit 1
fi
rm *.o
mv sched150 ../../sched
ls -ltr ../../sched
