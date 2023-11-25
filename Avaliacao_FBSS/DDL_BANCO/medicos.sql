CREATE TABLE medicos (
	id_especialidade int4 NULL,
	cpf numeric NOT NULL,
	nome varchar NULL,
	crm varchar NULL,
	CONSTRAINT medicos_pk PRIMARY KEY (cpf)
);