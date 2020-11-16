$Uri = 'https://localhost:5001/people'

$AddNewPerson = {
	$FirstName = Read-Host -Prompt 'FirstName'
	$LastName = Read-Host -Prompt 'LastName'
	$Address = Read-Host -Prompt 'Address'
	$Age = Read-Host -Prompt 'Age'
	$Interests = Read-Host -Prompt 'Interests'	
	
	$Body = @{
		Id = 0
		FirstName = $FirstName
		LastName = $LastName
		Address = $Address
		Age = $Age
		Interests = $Interests
	}
	
	Invoke-WebRequest -Method Post -Uri $Uri -Body ($Body|ConvertTo-Json) -ContentType "application/json"
	&$Retry
}

$Retry = {
	Write-Host ''
	$continue = Read-Host -Prompt 'Add another person? (y/n)'
	
	if ($continue -eq 'y') {
		Write-Host ''
		&$AddNewPerson
	} else {
		Write-Host 'Understood, have a nice day!'
		Start-Sleep -s 3
	}
}

&$AddNewPerson