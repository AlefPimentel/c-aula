const API = "https://localhost:7255/api/imovel"; // AJUSTA A PORTA SE PRECISAR

// =========================
// LISTAR
// =========================
async function listar() {
    const lista = document.getElementById("lista");
    if (!lista) return;

    lista.innerHTML = "<p>Carregando imóveis...</p>";

    try {
        const res = await fetch(API);
        if (!res.ok) throw new Error("Erro na API");

        const data = await res.json();

        lista.innerHTML = "";

        if (!data || data.length === 0) {
            lista.innerHTML = "<p>Nenhum imóvel cadastrado.</p>";
            return;
        }

        data.forEach(i => {
            const preco = Number(i.preco).toLocaleString("pt-BR", {
                style: "currency",
                currency: "BRL"
            });

            lista.innerHTML += `
                <div class="card">
                    <h3>${i.titulo}</h3>
                    <p><b>Tipo:</b> ${i.tipo}</p>
                    <p><b>Preço:</b> ${preco}</p>

                    <div class="actions">
                        <a href="editar.html?id=${i.id}">Editar</a>
                        <button onclick="deletar(${i.id})">Excluir</button>
                    </div>
                </div>
            `;
        });

    } catch (error) {
        console.error(error);
        lista.innerHTML = "<p style='color:red;'>Erro ao carregar imóveis.</p>";
    }
}

// =========================
// CRIAR
// =========================
async function criar() {
    const imovel = {
        id: parseInt(document.getElementById("id").value),
        titulo: document.getElementById("titulo").value,
        tipo: document.getElementById("tipo").value,
        preco: parseFloat(document.getElementById("preco").value)
    };

    try {
        const res = await fetch(API, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(imovel)
        });

        if (!res.ok) throw new Error("Erro ao criar");

        alert("Imóvel cadastrado com sucesso!");
        window.location.href = "index.html";

    } catch (error) {
        console.error(error);
        alert("Erro ao cadastrar imóvel.");
    }
}

// =========================
// DELETAR
// =========================
async function deletar(id) {
    const confirmar = confirm("Tem certeza que deseja excluir?");

    if (!confirmar) return;

    try {
        const res = await fetch(`${API}/${id}`, {
            method: "DELETE"
        });

        if (!res.ok) throw new Error("Erro ao deletar");

        listar();

    } catch (error) {
        console.error(error);
        alert("Erro ao excluir imóvel.");
    }
}

// =========================
// CARREGAR PARA EDIÇÃO
// =========================
async function carregar() {
    const params = new URLSearchParams(window.location.search);
    const id = params.get("id");

    if (!id) return;

    try {
        const res = await fetch(API);
        const data = await res.json();

        const imovel = data.find(x => x.id == id);

        if (!imovel) {
            alert("Imóvel não encontrado");
            window.location.href = "index.html";
            return;
        }

        document.getElementById("id").value = imovel.id;
        document.getElementById("titulo").value = imovel.titulo;
        document.getElementById("tipo").value = imovel.tipo;
        document.getElementById("preco").value = imovel.preco;

    } catch (error) {
        console.error(error);
        alert("Erro ao carregar imóvel.");
    }
}

// =========================
// ATUALIZAR
// =========================
async function atualizar() {
    const id = document.getElementById("id").value;

    const imovel = {
        id: parseInt(id),
        titulo: document.getElementById("titulo").value,
        tipo: document.getElementById("tipo").value,
        preco: parseFloat(document.getElementById("preco").value)
    };

    try {
        const res = await fetch(`${API}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(imovel)
        });

        if (!res.ok) throw new Error("Erro ao atualizar");

        alert("Imóvel atualizado com sucesso!");
        window.location.href = "index.html";

    } catch (error) {
        console.error(error);
        alert("Erro ao atualizar imóvel.");
    }
}