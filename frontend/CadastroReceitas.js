import React, { useState, useEffect } from 'react';

function CadastroReceita({ receita, onFinalizar }) {
  const [form, setForm] = useState({
    nome: '',
    descricao: '',
    ingredientes: '',
    modoPreparo:'',
    tempoPreparoMin: '',
    alergias: ''
  });

  useEffect(() => {
    if (receita) {
        setForm({
            nome: receita.nome,
            descricao: receita.descricao,
            ingredientes: receita.ingredientes.join(','),
            modoPreparo: receita.modoPreparo,
            tempoPreparoMin: receita.temoPreparoMin,
            alergias: receita.alergias.map(a => a.nome).join(',')
        });
    }
   }, [receita]);

   function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value});
   }

   async function handleSumbit(e) {
    e.preventDefault();
    const dados = {
        nome: form.nome,
        descricao: form.descricao,
        
    }
   }