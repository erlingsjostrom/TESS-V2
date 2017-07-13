export class Config {
  public static API_HOST: string = 'http://localhost';
  public static API_PORT: string = '51900';
  public static API_PREFIX: string = 'odata';
  public static API_VERSION: string = 'v1.0';

  public static API_URL = Config.getCompleteUrl(); 
  public static API_HEADERS = {
    Accept: 'application/json;odata.metadata=none',
  }
  private static getCompleteUrl() : string {
    return  Config.API_HOST + ':' + 
            Config.API_PORT + '/' +
            Config.API_PREFIX + '/' +
            Config.API_VERSION + '/';
  }
}