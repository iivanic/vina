#chmod 755 install.sh

docker compose up

#create a store
docker exec -it boundless-admin-1 ./shell.ts selfHosted install

# boundless-admin-1 is name of the container with Admin service. 
# In some circumstances, your local containers names might differ,
# in this case execute: docker ps and find name of the Admin service

#the services will be  available on the following ports:

#    http://localhost:3000 - Admin panel
#    http://localhost:3001 - Nginx server with Static assets
#    http://localhost:3002 - Service for image resizer
#    http://localhost:3003 - API service