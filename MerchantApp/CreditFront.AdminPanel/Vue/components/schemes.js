
Vue.component("schemes", {
    template: '#schemes-template',
    props: ['selecteditem', 'password'],
    data: function () {
        return {
            error: '',
            offset: 0,
            count: 10,
            searchQuery: '',
            items: []
        };
    },
    methods: {
        setError: function (error) {
            console.log('Error!:\n' + error);
            this.error = error.Code || error;
        },
        getItems: function () {
            var vue = this;
            $.post("/SchemeApi/GetItems",
                { offset: this.offset, count: this.count, searchQuery: this.searchQuery }).then(function (r) {
                    if (!r.Error) {
                        vue.items = r.Data;
                    }
                    else {
                        vue.setError(r.Error);
                    }
                });
        },
        editScheme: function (item) {
            console.log(item);
            console.log(item.Id);
            //this.selecteditem = item;

            //window.location.replace("/Cabinet/SchemeDetails/" + item.id);
        },
        deleteScheme: function (id) {
            window.location.replace("/Cabinet/SchemeDetails/" + id);
        }   
        
    },
    beforeMount() {
        this.getItems();
    }
});