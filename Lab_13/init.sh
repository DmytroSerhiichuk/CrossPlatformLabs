sudo apt-get update -y
sudo apt-get install -y wget apt-transport-https software-properties-common

# .NET 6.0
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y dotnet-sdk-6.0

echo 'export PATH=$PATH:$HOME/.dotnet/tools' >> ~/.bashrc
export PATH=$PATH:$HOME/.dotnet/tools

# Node
curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -
sudo apt-get install -y nodejs

# Folders
rm -rf /home/vagrant/Lab_13
rm -rf /home/vagrant/LabLib

mkdir -p /home/vagrant/Lab_13
mkdir -p /home/vagrant/LabLib

cp -r /vagrant/Lab_13/* /home/vagrant/Lab_13/
cp -r /vagrant/LabLib/* /home/vagrant/LabLib/

# Init node packages
cd /home/vagrant/Lab_13/client/
rm -rf ./node_modules
sudo npm install
echo "Completed"