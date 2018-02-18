import { Injectable } from '@angular/core';
import { Configuration } from '../../swagger-gen';

export const AccessTokenStorageKey = 'access_token';

@Injectable()
export class AuthService {
  private static nonScopeClaims = ['unique_name', 'accountId', 'nbf', 'exp', 'iat'];
  private tokenData: any;
  constructor(private config: Configuration) {
  }

  public IsAuthenticated(): boolean {
    return !!this.GetTokenData();
  }

  public GetTokenData(): any | undefined {
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
    this.tokenData = payload;
    return payload;
  }

  public GetScopes(): string[] {
    const payload = this.GetTokenData();
    if (!payload) {
      return [];
    }
    const scopes = [];
    const keys = Object.keys(payload);
    keys.forEach(key => {
      if (AuthService.nonScopeClaims.indexOf(key) < 0) {
        scopes.push(key);
      }
    });
    return scopes;
  }

  public Login(token: string) {
    localStorage.setItem(AccessTokenStorageKey, token);
  }
  public Logout() {
    localStorage.removeItem(AccessTokenStorageKey);
    window.location.reload();
  }
}
