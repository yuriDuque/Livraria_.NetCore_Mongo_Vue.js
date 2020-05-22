<template>
  <div class="tela-inteira centralizar backgroud">
    <b-form @submit="onSubmit" class="container p-3">
      <h4>Login</h4>
      <p>Seja bem vindo, acesse sua conta!</p>

      <b-form-group label="Login:">
        <b-form-input v-model="form.login" required></b-form-input>
      </b-form-group>

      <b-form-group label="Senha:">
        <b-form-input v-model="form.senha" type="password" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-4">
        <b-form-checkbox-group v-model="form.checked">
          <b-form-checkbox value="save">Guarde meu login</b-form-checkbox>
        </b-form-checkbox-group>
      </b-form-group>

      <label v-if="error" class="label-erro">{{error}}</label>

      <b-button class="mt-3 w-100 backgroud-primary backgroud-primary-hover b" type="submit">Entrar</b-button>
    </b-form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        login: "admin",
        senha: "admin",
        checked: []
      },

      error: ""
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      debugger;

      if (this.form.login === "admin" && this.form.senha === "admin") {
        this.$router.push({ name: "admin" });
      } else {
        this.error = "Dados incorretos!";
      }
    }
  }
};
</script>

<style scoped>
.backgroud {
  background-color: rgb(237, 237, 239);
}

.tela-inteira {
  height: 100vh;
}

.centralizar {
  align-items: center;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;
}

.container {
  background-color: white;
  width: 350px;
  border-radius: 15px;
  color: #52536a;
}
</style>
