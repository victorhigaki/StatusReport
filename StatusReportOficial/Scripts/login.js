function Login() {
  
  //entra Login e senha se erro ou certo
  var done=0;
  var usuario = document.getElementsByName('usuario')[0].value;
  usuario=usuario.toLowerCase();
  var senha= document.getElementsByName('senha')[0].value;
  senha=senha.toLowerCase();
  if (usuario=="resource" && senha=="@123") {
    window.location="bem-vindo.html";
    done=1;
  }
  if (done==0) { alert("Dados incorretos, tente novamente"); }

  // // Clicar Entrar (Techado)
  
}
var input = document.getElementById("senha");

  input.addEventListener("keyup", function(event) {
    event.preventDefault();

    if (event.keyCode === 13) {
      document.getElementById("entrar").click();
    }
});
  