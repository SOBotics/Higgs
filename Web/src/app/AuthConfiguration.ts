import { Configuration } from '../swagger-gen';

export class AuthConfiguration extends Configuration {
    constructor() {
        super();
        this.accessToken = localStorage.getItem('access_token');
    }
}
