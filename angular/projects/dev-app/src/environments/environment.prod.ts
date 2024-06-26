import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'DynamicMenu',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44348/',
    redirectUri: baseUrl,
    clientId: 'DynamicMenu_App',
    responseType: 'code',
    scope: 'offline_access DynamicMenu',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44348',
      rootNamespace: 'JS.Abp.DynamicMenu',
    },
    DynamicMenu: {
      url: 'https://localhost:44386',
      rootNamespace: 'JS.Abp.DynamicMenu',
    },
  },
} as Environment;
