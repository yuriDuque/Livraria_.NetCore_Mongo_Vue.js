<template>
  <b-container class="mt-3">
    <div class="row mb-4">
      <h3 class="col">Livros</h3>
      <b-button
        class="col-2 w-100 backgroud-primary backgroud-primary-hover b"
        type="submit"
      >Novo Livro</b-button>
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
        { id: 1, titulo: "Livro 1", autor: "Autor 1", ações: null },
        { id: 2, titulo: "Livro 2", autor: "Autor 1", ações: null },
        { id: 3, titulo: "Livro 3", autor: "Autor 2", ações: null },
        { id: 4, titulo: "Livro 4", autor: "Autor 3", ações: null }
      ],
      fields: [
        { key: "id", label: "Id" },
        { key: "titulo", label: "Titulo" },
        { key: "autor", label: "Autor" },
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