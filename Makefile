ef-cli: 
	dotnet tool install dotnet-ef --version 8.0.2 --allow-downgrade --create-manifest-if-needed

run-database:
	docker-compose -f docker-compose.yml -p nomiki-api up

add-init-migrations:
	dotnet ef migrations add Init --project Nomiki.Api -o InterestRate/Database/Migrations -v