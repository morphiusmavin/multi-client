if [[ -z "${CROSS_COMPILE}" ]]; then
  echo "run setARMpath.sh"
  exit 1
fi
if [ -e ~/thread_server.zip ]
then
unzip -o ~/thread_server.zip
mv mytypes.h ..
make clean
make &> out.txt

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

#ftp 192.168.88.145
scp sched 192.168.88.146:/home/dan
rm *.o
clear
cat out.txt | grep warning
mv ~/thread_server.zip ~/thread_server.old.zip
else
echo "thread_server.zip not found"
fi
