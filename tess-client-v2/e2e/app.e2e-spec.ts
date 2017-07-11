import { TessClientV2Page } from './app.po';

describe('tess-client-v2 App', () => {
  let page: TessClientV2Page;

  beforeEach(() => {
    page = new TessClientV2Page();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
