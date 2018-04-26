import { Injectable } from '@angular/core';
import { Configuration } from '../../swagger-gen';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';

export const AccessTokenStorageKey = 'access_token';

export interface AuthDetails {
  RawToken: any;
  IsAuthenticated: boolean;
  GetScopes: () => string[];
}

@Injectable()
export class AuthService {
  private static nonScopeClaims = ['unique_name', 'accountId', 'nbf', 'exp', 'iat'];

  private subject: BehaviorSubject<AuthDetails> = new BehaviorSubject<AuthDetails>(this.getAuthDetails());
  constructor(private config: Configuration) {
  }

  public GetAuthDetails(): Observable<AuthDetails> {
    return this.subject;
  }

  private getAuthDetails(): AuthDetails {
    const getTokenData = () => {
      const accessTokenGetter = this.config.accessToken;
      if (!accessTokenGetter) {
        return undefined;
      }
      let accessToken: string;
      if (typeof accessTokenGetter === 'function') {
        accessToken = accessTokenGetter();
      } else {
        accessToken = accessTokenGetter;
      }
      if (!accessToken) {
        return undefined;
      }
      const tokenPayload = accessToken.split('.')[1];
      const payload = JSON.parse(atob(tokenPayload));
      return payload;
    };
    const tokenData = getTokenData();
    return {
      RawToken: tokenData,
      IsAuthenticated: !!tokenData,
      GetScopes: () => {
        if (!tokenData) {
          return [];
        }
        const scopes = [];
        const keys = Object.keys(tokenData);
        keys.forEach(key => {
          if (AuthService.nonScopeClaims.indexOf(key) < 0) {
            scopes.push(key);
          }
        });
        return scopes;
      }
    };
  }

  public Login(token: string) {
    localStorage.setItem(AccessTokenStorageKey, token);
    this.subject.next(this.getAuthDetails());
  }
  public Logout() {
    localStorage.removeItem(AccessTokenStorageKey);
    this.subject.next(this.getAuthDetails());
  }
}
