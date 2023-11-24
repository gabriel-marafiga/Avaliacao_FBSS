$(document).ready(function () {
        LoadAll()
})


function LoadAll() {
    fetch("Medicos/ListAll")
        .then((medicos) => medicos.json())
        .then(popular);
    }


function popular(medicos) {
    var indice = 0;
    var divTabela = document.getElementById("divTabela");
    var tabela = '<table class="table table-sm table-hover table-striped tabela">';
    tabela += '<thead>';
    tabela += '<tr>';
    tabela += '<th>Nome</th>';
    tabela += '<th>CPF</th>';
    tabela += '<th>CRM</th>';
    tabela += '<th>Especialidade</th>';
    tabela += '</tr>';
    tabela += '</thead>';
    tabela += '<tbody>';

    console.log(medicos)

    for (indice = 0; indice < medicos.length; indice++) {
        tabela += `<tr id="${medicos[indice].nome}">`;
        tabela += `<td>${medicos[indice].nome}</td>`;
        tabela += `<td>${medicos[indice].cpf}</td>`;
        tabela += `<td>${medicos[indice].crm}</td>`;
        tabela += `<td>${medicos[indice].especialidade}</td>`;
        tabela += `<td><button class="btn btn-sm btn-outline-info" onclick="pegarMedicoPelocpf(${medicos[indice].cpf})">Atualizar</button> 
                        <button class="btn btn-sm btn-outline-danger" onclick="excluirMedico(${medicos[indice].cpf})">Excluir</button></td>`;
        tabela += '</tr>';
    }
    tabela += '</tbody>';
    tabela += '</table>';

    divTabela.innerHTML = tabela;
}

function pegarMedicoPelocpf(cpf) {
    fetch("Medicos/List?cpf=" + cpf)
        .then((medicos) => {
            const json = medicos.json()
            
        });        
}

function excluirMedico(cpf) {
    fetch("Medicos/Delete?cpf=" + cpf)
        .then((response) => {
            if (response.ok) 
                LoadAll();
            else
                alert(Response.json().mensagem)
        })

}

