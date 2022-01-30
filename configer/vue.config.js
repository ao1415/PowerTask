module.exports = {
  pluginOptions: {
    electronBuilder: {
      nodeIntegration: true,
      builderOptions: {
        win: {
          icon: 'src/assets/PowerTask.png',
          target: [
            {
              target: "portable",
              arch: ["x64"],
            },
          ],
        },
      },
    },
  },
};
