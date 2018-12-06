
Vue.component("login", {
    template: '#login-template',
    //props: ['login', 'password'],
    data: function () {
        return {
            login: '',
            password: '',
            error : ''
        };
    },
    methods: {
        setError: function (error) {
            console.log('Error!:\n' + error);
            this.error = error.Code || error;
        },
        auth: function () {
            console.log('login');
            console.log(this.login + " " + this.password);
            var vue = this;
            $.post("/SecurityApi/Authenticate",
                { login: this.login, password: this.password })
                .then(function (r) {
                    console.log(r);
                    if (!r.Error) {
                        document.location.replace("/Cabinet/Dashboard");
                    }
                    else {
                        vue.setError(r.Error);
                    }
            });
        }
    }
});