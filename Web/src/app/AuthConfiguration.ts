import { Configuration } from '../swagger-gen';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthConfiguration extends Configuration {
    constructor() {
        super({
            accessToken: () => localStorage.getItem('access_token')
        });
    }
}
