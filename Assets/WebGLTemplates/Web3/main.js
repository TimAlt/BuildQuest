async function login(){
  const accounts = await ethereum.request({ method: 'eth_requestAccounts' });
  const account = accounts[0];
  console.log(account)
  window.unstableSorcery.SendMessage('Web3', 'Account', account)
}