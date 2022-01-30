<template>
  <div class="d-flex h-100">
    <div class="h-100 p-1 border sidebar">
      <div class="list-group">
        <a
          class="list-group-item list-group-item-action"
          :class="{ active: selected == 'WinCron' }"
          @click="selected = 'WinCron'"
          ><i class="bi bi-list-task me-1"></i>WinCron</a
        >
      </div>
    </div>
    <div class="h-100 w-100">
      <div v-if="selected == 'WinCron'" class="h-100"><WinCron /></div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import WinCron from "./WinCron.vue";

export default defineComponent({
  name: "ConfigLayout",
  components: {
    WinCron,
  },
  setup() {
    null;
  },
  data() {
    return {
      selected: "WinCron",
    };
  },
});
</script>

<style scoped>
.sidebar {
  width: 15em;
  min-width: 15em;
}
</style>
