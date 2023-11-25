$(document).ready(function () {
    LoadAll();
});



$(".novoMedico").click(() => {
    limparModal()
    BuscaEPopulaEspecialidade();
    mostrarMedicoModal("Novo Medico");
});

function BuscaEPopulaEspecialidade () {
        var select = document.getElementById("SelecaoEspecialidade");
        fetch("Especialidade/ListAll")
            .then((especialidade) => especialidade.json())
            .then(populaSelecionarEspecialidade);
    
}

$(".btnSalvar").click(function () {
    let especialidade_selecionada = document.getElementById("SelecaoEspecialidade").value

    if (especialidade_selecionada == 0) {
        alert("Selecione uma especialide")
    }

    var medico = {
        CPF: $('.CPF').val(),
        nome: $('.nome').val(),
        CRM: $('.CRM').val(),
        id_especialidade: parseInt(especialidade_selecionada)
    };

    console.log($('._cpf').val())

    if ($('.CPF').val() )
    fetch("Medicos/Includ",
        {
            method: "POST",
            body: JSON.stringify(medico),
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => { 
            if (response.ok) {
                LoadAll();
                $("[data-dismiss=modal]").trigger({ type: "click" });
            }
        })
});

function pegarMedicoPelocpf(cpf) {
    fetch("Medicos/List?cpf=" + cpf)
        .then((medico) => (medico.json())
            .then((medico) => {
                BuscaEPopulaEspecialidade();
                
                PopulaModal(medico);
                mostrarMedicoModal("Editando " + medico.nome);
            })
        )
}

function LoadAll() {
    fetch("Medicos/ListAll")
        .then((medicos) => medicos.json())
        .then(popular);
}

function populaSelecionarEspecialidade(especialidades) {
    var select = document.getElementById("SelecaoEspecialidade");
    select.innerHTML = ''
    var el = document.createElement("option");
    el.textContent = "Selecionar";
    el.value = 0;
    select.appendChild(el);

    for (var i = 0; i < especialidades.length; i++) {
        var opt = especialidades[i];
        var el = document.createElement("option");
        el.textContent = opt.Descricao;
        el.value = opt.Id;
        select.appendChild(el);
    }
}

function popular(medicos) {
    console.log(medicos)
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



function excluirMedico(cpf) {
    fetch("Medicos/Delete?cpf=" + cpf)
        .then((response) => {
            if (response.ok)
                LoadAll();
            else
                alert(Response.json().mensagem)
        })

}

function mostrarMedicoModal(texto) {
    
    if (texto != null) {
        $(".modal-title").text(texto);
    }
    var myModal = new bootstrap.Modal(document.getElementById("modal"), {});
    myModal.show();
}

function limparModal() {
    $("._cpf").val('');
    $(".CPF").val('');
    $(".nome").val('');
    $(".CRM").val('');
}

function PopulaModal(json_medico) {
    limparModal();
    console.log(json_medico);
    $("_cpf").val(json_medico.cpf);
    $(".CPF").val(json_medico.cpf);
    $(".nome").val(json_medico.nome);
    $(".CRM").val(json_medico.crm);
 
    document.getElementById("SelecaoEspecialidade").value = json_medico.id_especialidade
}