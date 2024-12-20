#ls dat_files > dat.txt
#cat dat.txt > out.txt
# 5th param is silent mode
#./test2d dat3.dat dat.txt $1 $2 0 > out.txt
./test2d dat3.dat dat.txt $1 $2 1 > out.txt
# 2nd param is silent mode
#./test2b dat3.dat 0 >> out.txt
./test2b dat3.dat 1 >> out.txt
cat out.txt | head -n 10
echo " "
cat out.txt | tail -n 10
ls -ltr *.dat
#echo $1 $2
echo "lines in out.txt"
wc -l out.txt
rm dat3.dat
