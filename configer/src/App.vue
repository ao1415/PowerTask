<template>
  <ConfigLayout />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import ConfigLayout from "./components/ConfigLayout.vue";

export default defineComponent({
  name: "App",
  components: {
    ConfigLayout,
  },
});
</script>

<style>
html,
body,
#app {
  height: 100%;
}
</style>
