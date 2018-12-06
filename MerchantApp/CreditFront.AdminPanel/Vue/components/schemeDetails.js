
Vue.component("schemedetails", {
    template: '#schemeDetails-template',
    props: ['itemid', 'schemeitem'],
    data: function () {
        return {
            error: '',            
            item: {},
            id: ''
        };
    },
    methods: {
        setError: function (error) {
            console.log('Error!:\n' + error);
            this.error = error.Code || error;
        },
        getItem: function (id) {
            var vue = this;
            $.post("/SchemeApi/GetItem",
                { id: id }).then(function (r) {
                    if (!r.Error) {
                        vue.item = r.Data;
                    }
                    else {
                        vue.setError(r.Error);
                    }
                });
        }     
    },
    beforeMount() {

        if (this.item) {
            if (this.schemeitem)
                this.getItem(this.schemeitem.Id);
        }
        else {
            item = { Id: this.schemeitem.Id, Name: this.schemeitem.Name };
        }
    }
});