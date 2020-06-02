<template>
  <b-container class="mt-3">
    <div class="row mb-4">
      <h3 class="col">Usuários</h3>
      <b-button
        class="col-2 w-100 backgroud-primary backgroud-primary-hover b"
        type="submit"
      >Novo Usuário</b-button>
    </div>

    <b-table small stacked="md" :items="items" :fields="fields">
      <template v-slot:cell(actions)="row">
        <b-icon class="mr-2 pointer" icon="pencil" @click="info(row.item)"></b-icon>
        <b-icon class="ml-2 pointer" icon="bucket" @click="info(row.item)"></b-icon>
      </template>
    </b-table>
  </b-container>
</template>

<script>
export default {
  data() {
    return {
      items: [
        { id: 40, nome: "Dickerson", sobrenome: "Macdonald"},
        { id: 21, nome: "Larsen", sobrenome: "Shaw"},
        { id: 89, nome: "Geneva", sobrenome: "Wilson"},
        { id: 38, nome: "Jami", sobrenome: "Carney"}
      ],
      fields: [
        { key: "id", label: "Id" },
        { key: "nome", label: "Nome" },
        { key: "sobrenome", label: "Sobrenome" },
        { key: "actions", label: "Actions" }
      ],
    };
  },
  mounted() {
  },
  methods: {
    info(item, index, button) {
      this.infoModal.title = `Row index: ${index}`;
      this.infoModal.content = JSON.stringify(item, null, 2);
      this.$root.$emit("bv::show::modal", this.infoModal.id, button);
    },
  }
};
</script>