function OpenClose(){
	var x = document.getElementById("painel");
	if (x.style.width === "370px"){
		x.style.width = "0";
	} else {
		x.style.width = "370px";
	}
	var bk = document.getElementById("tela");
	if (bk.style.background === "rgba(0, 0, 0, 0.4)"){
		bk.style.background = "rgba(0, 0, 0, 0)";
	} else {
		bk.style.background = "rgba(0, 0, 0, 0.4)";
	}
}
