$Uri = 'https://localhost:5001/people?query='

$RunQuery = {
	$Query = Read-Host -Prompt 'Query'
	Invoke-WebRequest -Method Get -Uri ($Uri + $Query)
	&$GetUserInput
}

$GetUserInput = {
	Write-Host ''
	$continue = Read-Host -Prompt 'Search again? (y/n)'
	
	if ($continue -eq 'y') {
		Write-Host ''
		&$RunQuery
	} else {
		Write-Host 'Understood, have a nice day!'
		Start-Sleep -s 3
	}
}

# First Run
&$RunQuery

