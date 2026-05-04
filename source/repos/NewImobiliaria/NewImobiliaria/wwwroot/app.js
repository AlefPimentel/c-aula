const API = "https://localhost:7255/api/imovel";

const money = (v) => Number(v || 0).toLocaleString("pt-BR", { style: "currency", currency: "BRL" });

async function fetchImoveis() {
  const res = await fetch(API);
  if (!res.ok) throw new Error("Erro na API");
  return res.json();
}

function card(i) {
  return `<article class="card"><h3>${i.titulo}</h3><p><b>${i.tipo}</b> • ${i.cidade || "Cidade não informada"}</p><p>${money(i.preco)} • ${i.area || "-"}m² • ${i.quartos || "-"} quartos</p><div class="actions"><a href="pdp.html?id=${i.id}">Ver PDP</a><a href="editar.html?id=${i.id}">Editar</a><button onclick="deletar(${i.id})">Excluir</button></div></article>`;
}

async function listarShelf() {
  const el = document.getElementById("shelf"); if (!el) return;
  const data = await fetchImoveis();
  el.innerHTML = data.slice(0, 4).map(card).join("") || "<p>Nenhum imóvel disponível.</p>";
}

async function listarPLP() {
  const el = document.getElementById("plpList"); if (!el) return;
  const cidade = (document.getElementById("fCidade")?.value || "").toLowerCase();
  const tipo = document.getElementById("fTipo")?.value || "";
  const data = await fetchImoveis();
  const filtered = data.filter(i => (!cidade || (i.cidade || "").toLowerCase().includes(cidade)) && (!tipo || i.tipo === tipo));
  el.innerHTML = filtered.map(card).join("") || "<p>Nenhum resultado para os filtros.</p>";
}

async function carregarPDP() {
  const el = document.getElementById("pdp"); if (!el) return;
  const id = new URLSearchParams(window.location.search).get("id");
  const data = await fetchImoveis();
  const i = data.find(x => String(x.id) === String(id));
  if (!i) { el.innerHTML = "<p>Imóvel não encontrado.</p>"; return; }
  el.innerHTML = `<article class="card"><h1>${i.titulo}</h1><p><b>Tipo:</b> ${i.tipo}</p><p><b>Cidade:</b> ${i.cidade || "-"}</p><p><b>Área:</b> ${i.area || "-"} m²</p><p><b>Quartos:</b> ${i.quartos || "-"}</p><p><b>Preço:</b> ${money(i.preco)}</p></article>`;
}

function payloadFromForm() {
  return {
    id: parseInt(document.getElementById("id").value),
    titulo: document.getElementById("titulo").value,
    tipo: document.getElementById("tipo").value,
    preco: parseFloat(document.getElementById("preco").value),
    cidade: document.getElementById("cidade").value,
    area: parseFloat(document.getElementById("area").value) || null,
    quartos: parseInt(document.getElementById("quartos").value) || null
  };
}

async function criar() { const res = await fetch(API,{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify(payloadFromForm())}); if(!res.ok) return alert("Erro ao criar imóvel"); window.location.href="plp.html"; }
async function atualizar() { const id=document.getElementById("id").value; const res=await fetch(`${API}/${id}`,{method:"PUT",headers:{"Content-Type":"application/json"},body:JSON.stringify(payloadFromForm())}); if(!res.ok) return alert("Erro ao atualizar imóvel"); window.location.href="plp.html"; }
async function deletar(id) { if(!confirm("Deseja excluir este imóvel?")) return; const res=await fetch(`${API}/${id}`,{method:"DELETE"}); if(!res.ok) return alert("Erro ao excluir imóvel"); listarPLP(); listarShelf(); }

async function carregar() {
  const id = new URLSearchParams(window.location.search).get("id");
  const data = await fetchImoveis();
  const i = data.find(x => String(x.id) === String(id));
  if (!i) return;
  ["id","titulo","tipo","preco","cidade","area","quartos"].forEach(k => {
    const el = document.getElementById(k); if (el) el.value = i[k] ?? "";
  });
}
