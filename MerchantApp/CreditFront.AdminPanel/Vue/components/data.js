
Vue.component("data", {
    template: '#data-template',
    //props: ['login', 'password'],
    data: function () {
        return {
            error : ''
        };
    },
    methods: {
        setError: function (error) {
            console.log('Error!:\n' + error);
            this.error = error.Code || error;
        }
        
    }
});