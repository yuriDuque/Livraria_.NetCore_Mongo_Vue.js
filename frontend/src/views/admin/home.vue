<template>
  <div class="menu backgroud-secundary">
    <div class="titulo-menu d-flex justify-content-between">
      <h2 class="titulo mb-5">Livraria</h2>
      <b-icon class="icon icon-button h2" icon="arrow-right"></b-icon>
    </div>

    <div
      v-for="(item, index) in itensMenu"
      :key="index"
      :class="item.selecionado ? 'item-menu item-menu-selecionado color-primary' : 'item-menu'"
      @click="selecionarItem(index)"
    >
      <p class="label-menu d-flex">
        <b-icon class="icon h4" :icon="item.icon"></b-icon>
        {{item.label}}
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: "",

  data() {
    return {
      titulo: "Livraria",
      itensMenu: [
        { label: "Livros", icon: "book", rota: "login", selecionado: true },
        { label: "Clientes", icon: "person", rota: "usuario", selecionado: false }
      ]
    };
  },

  methods: {
    selecionarItem(index) {
      this.itensMenu.forEach(element => {
        element.selecionado = false;
      });

      this.itensMenu[index].selecionado = this.itensMenu[index].selecionado
        ? false
        : true;

      this.$router.push({ name: this.itensMenu[index].rota });
    }
  }
};
</script>

<style scoped>
.menu {
  width: 300px;
  height: 100vh;
  padding: 20px;
  color: white;
}

.titulo {
  font-weight: bold;
}

.icon-button {
  transition: transform 0.5s linear;
}
.icon-button:hover {
  transform: translate(25%, 0);
  transition: transform 0.5s linear;
  cursor: pointer;
}

.item-menu {
  cursor: pointer;
  margin: 15px 0;
  padding-left: 10px;
  border-radius: 12px;
  display: flex;
}

.item-menu-selecionado {
  background-color: white;
  display: flex;
  box-shadow: 0 0 0 5px rgb(255, 255, 255, 0.2);
}

.icon {
  margin-top: 5px;
  margin-right: 10px;
  margin-bottom: 0px;
}

.label-menu {
  width: 220px;
  font-size: 22px;
  margin: 4px;
  transition: transform 0.5s linear;
}

.label-menu:hover {
  transform: translate(5%, 0);
  transition: transform 0.5s linear;
  cursor: pointer;
}
</style>