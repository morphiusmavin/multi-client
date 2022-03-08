if [[ -z "${CROSS_COMPILE}" ]]; then
  echo "run setARMpath.sh"
  exit 1
fi
if [ -e ~/thread_client.zip ]
then
unzip -o ~/thread_client.zip
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

ftp 192.168.88.145
#scp sched 192.168.88.147:/home/dan
rm *.o
clear
cat out.txt | grep warning
mv ~/thread_client.zip ~/thread_client.old.zip
else
echo "thread_client.zip not found"
fi
