param([string]$resourceGroup)

$adminCredential = Get-Credential -Message "Enter a username and password for the VM administrator."

For ($i = 1; $i -le 3; $i++) {
    <#create a name for each VM and store it in a variable and output it to the console:#>
    $vmName = "ConferenceDemo" + $i
    Write-Host "Creating VM: " $vmName
    <#Create the vm #>
    New-AzVm -ResourceGroupName $resourceGroup -Name $vmName -Credential $adminCredential -Image UbuntuLTS

}
