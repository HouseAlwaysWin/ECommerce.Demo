var vueModal = new Vue({
    el: "#vue-modal",
    data: {
        modalStyleObj: {
            display: "none",
        },
        modalText: {
            title: '',
            body: '',
            confirmBtn: 'Confirm',
            cancelBtn: 'Cancel'
        },
        modalShowInfo: {
            title: true,
            confirmBtn: true,
            cancelBtn: false
        },
        modelEvent: {
            confirm: null,
        }
    },
    methods: {
        show: function (options) {
            var self = this;
            if (self.modalStyleObj.display === "block") {
                return;
            }

            self.modalStyleObj.display = "block";
            self.modalText = {
                title: options.titleText || self.modalText.title,
                body: options.bodyText || self.modalText.body,
                confirmBtn: options.confirmBtnText || self.modalText.confirmBtn,
                cancelBtn: options.cancelBtnText || self.modalText.cancelBtn
            };

            self.modalShowInfo = {
                title: options.showTitleText || self.modalShowInfo.title,
                confirmBtn: options.showConfirmBtn || self.modalShowInfo.confirmBtn,
                cancelBtn: options.showCancelBtn || self.modalShowInfo.cancelBtn
            };

            return new Promise(function (resolve, reject) {
                self.modelEvent.confirm = resolve;
            });
        },
        close: function () {
            var self = this;
            self.modalStyleObj.display = "none";
            self.modelEvent.confirm(false);
            self.resetData();
        },
        confirm: function () {
            var self = this;
            self.modelEvent.confirm(true);
            self.resetData();
            self.close();
        },
        cancel: function () {
            var self = this;
            self.resetData();
            self.close();
        },
        resetData: function () {
            var self = this;
            self.data = {
                modalStyleObj: {
                    display: "none",
                },
                modalText: {
                    title: '',
                    body: '',
                    confirmBtn: 'Confirm',
                    cancelBtn: 'Cancel'
                },
                modalShowInfo: {
                    title: true,
                    confirmBtn: true,
                    cancelBtn: false
                },
                modelEvent: {
                    confirm: null,
                }
            }
        }
    }
});