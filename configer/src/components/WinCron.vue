<template>
  <div class="d-flex flex-column h-100">
    <ul class="list-group border list-region">
      <li class="list-group-item list-group-item-action disabled">
        <div class="row">
          <div class="col-md-1 border-end">有効</div>
          <div class="col-md-2 border-end">名前</div>
          <div class="col-md-2 border-end">説明</div>
          <div class="col-md-2 border-end">周期</div>
          <div class="col-md border-end">パス</div>
          <div class="col-md-2">引数</div>
        </div>
      </li>
      <li
        v-for="(item, index) in getConfigList"
        :key="item"
        class="list-group-item list-group-item-action"
        :class="{ active: item.active }"
        @click="onListClick(index)"
      >
        <div class="row">
          <div class="col-md-1 border-end">{{ item.enable }}</div>
          <div class="col-md-2 border-end">{{ item.name }}</div>
          <div class="col-md-2 border-end">{{ item.explain }}</div>
          <div class="col-md-2 border-end">{{ item.timing }}</div>
          <div class="col-md border-end">{{ item.path }}</div>
          <div class="col-md-2">{{ item.param }}</div>
        </div>
      </li>
    </ul>
    <div class="container pt-3 border form-region">
      <div class="row g-3 mb-3">
        <div class="col-md-auto">
          <div class="form-check form-switch">
            <input
              type="checkbox"
              id="input-enable"
              class="form-check-input"
              v-model="enable"
            />
            <label for="input-enable" class="form-label">有効</label>
          </div>
        </div>
        <div class="ms-auto col-md-auto">
          <button class="btn btn-primary" @click="onSaveClick()">保存</button>
        </div>
        <div class="col-md-auto">
          <button class="btn btn-secondary">ダイアログ</button>
        </div>
      </div>
      <div class="row g-3 mb-3">
        <div class="col-md-5">
          <label for="input-name" class="form-label">名前</label>
          <input
            type="text"
            id="input-name"
            class="form-control"
            placeholder="名前"
            v-model="name"
            :readonly="!enable"
          />
        </div>
        <div class="col-md-7">
          <label for="input-explain" class="form-label">説明</label>
          <input
            type="text"
            id="input-explain"
            class="form-control"
            placeholder="説明"
            v-model="explain"
            :readonly="!enable"
          />
        </div>
      </div>
      <div class="row g-3 mb-3">
        <div class="col-md-4">
          <label for="input-timing" class="form-label"
            >周期設定（秒分時日月週）</label
          >
          <input
            type="text"
            id="input-timing"
            class="form-control"
            placeholder="* * * * * *"
            v-model="timing"
            :readonly="!enable"
          />
        </div>
      </div>
      <div class="row g-3 mb-3">
        <div class="col-md-8">
          <label for="input-path" class="form-label"
            >スクリプトファイルパス</label
          >
          <input
            type="text"
            id="input-path"
            class="form-control"
            placeholder="ファイルパス"
            v-model="path"
            :readonly="!enable"
          />
        </div>
        <div class="col-md-4">
          <label for="input-param" class="form-label">スクリプト引数</label>
          <input
            type="text"
            id="input-param"
            class="form-control"
            placeholder="引数"
            v-model="param"
            :readonly="!enable"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import fs from "fs";
import path from "path";

type ConfigType = {
  enable: boolean;
  name: string;
  explain: string;
  timing: string;
  path: string;
  param: string;
  active: boolean;
};
type ConfigListType = ConfigType[];

type ConfigJsonType = {
  Name: string;
  Explain: string;
  Timing: string;
  Path: string;
  Param: string;
  Enable: boolean;
};
type ConfigJsonListType = ConfigJsonType[];

const ConfigFilePath = "./../Config/WinCron.json";
const ExePath = path.dirname(path.dirname(path.dirname(__dirname)));

export default defineComponent({
  setup() {
    null;
  },
  data(): {
    enable: boolean;
    name: string;
    explain: string;
    timing: string;
    path: string;
    param: string;
    configList: ConfigListType;
  } {
    return {
      enable: false,
      name: "",
      explain: "",
      timing: "",
      path: "",
      param: "",
      configList: [],
    };
  },
  created() {
    this.onLoadConfig();
  },
  computed: {
    isEnable(): boolean {
      return this.enable;
    },
    getConfigList(): ConfigListType {
      return this.configList;
    },
  },
  methods: {
    onListClick(index: number) {
      this.configList.forEach((element) => {
        element.active = false;
      });
      this.configList[index].active = true;

      this.enable = this.configList[index].enable;
      this.name = this.configList[index].name;
      this.explain = this.configList[index].explain;
      this.timing = this.configList[index].timing;
      this.path = this.configList[index].path;
      this.param = this.configList[index].param;
    },
    onSaveClick() {
      try {
        const jsonData: ConfigJsonListType = [];
        for (let i = 0; i < this.configList.length; i++) {
          if (this.configList[i].active) {
            jsonData.push({
              Enable: this.enable,
              Name: this.name,
              Explain: this.explain,
              Timing: this.timing,
              Path: this.path,
              Param: this.param,
            });
          } else {
            if (i < this.configList.length - 1) {
              jsonData.push({
                Enable: this.configList[i].enable,
                Name: this.configList[i].name,
                Explain: this.configList[i].explain,
                Timing: this.configList[i].timing,
                Path: this.configList[i].path,
                Param: this.configList[i].param,
              });
            }
          }
        }

        const filePath = path.join(ExePath, ConfigFilePath);
        fs.writeFileSync(filePath, JSON.stringify(jsonData, null, 2));

        this.onLoadConfig();
      } catch (error) {
        console.error("ファイル書き込み失敗", error);
      }
    },
    onLoadConfig(): void {
      try {
        const filePath = path.join(ExePath, ConfigFilePath);
        const text = fs.readFileSync(filePath, {
          encoding: "utf8",
        });
        const jsonData: ConfigJsonListType = JSON.parse(text);

        let configData: ConfigListType = [];

        jsonData.forEach((element: ConfigJsonType) => {
          configData.push({
            enable: element.Enable,
            name: element.Name,
            explain: element.Explain,
            timing: element.Timing,
            path: element.Path,
            param: element.Param,
            active: false,
          });
        });
        // 新規追加用の空設定
        configData.push({
          enable: false,
          name: "",
          explain: "",
          timing: "",
          path: "",
          param: "",
          active: false,
        });

        this.configList = configData;
      } catch (error) {
        console.error("ファイル読み込み失敗", error);
      }
    },
  },
});
</script>

<style scoped>
.list-region {
  overflow-y: scroll;
  min-height: 7em;
  height: calc(100% - 22em);
}
.form-region {
  overflow-y: scroll;
  min-height: 22em;
  max-height: 22em;
}
</style>
