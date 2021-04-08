# UniData - Data Management for Unity

**Created by Hiroya Aramaki ([Makihiro](https://twitter.com/makihiro_dev))**

## What is UniData ?
UniData is a data management system for Unity.

In addition to data management, it also useful for implementing Achievements, Quests, etc.

## <a id="index" href="#index"> Table of Contents </a>

- [Installation](#installation)
- [🔰 Usage](#usage)
- [External Assets Integration](#external-assets-integration)
  - [Easy Save](#external-assets-easysave)
  - [UniRx](#external-assets-unirx)
- [UniData Pro](#unidata-pro)
- [Author Info](#author-info)
- [License](#license)

## <a id="installation" href="#installation"> Installation </a>

Install this package from releases.

**UniData is currently a preview package.**

Releases: [https://github.com/mackysoft/UniData/releases](https://github.com/mackysoft/UniData/releases)

## <a id="usage" href="#usage"> 🔰 Usage </a>

being written

## <a id="external-assets-integration" href="#external-assets-integration"> External Assets Integration </a>

UniData supports integration with several external assets.

### <a id="external-assets-easysave3" href="#external-assets-easysave3"> Easy Save </a>

The Easy Save integration includes the `ES3DataCatalogIOAsset`.

This allows you to perform I/O operations using ES3 with no coding.

Easy Save: [https://assetstore.unity.com/packages/tools/input-management/easy-save-the-complete-save-load-asset-768](https://assetstore.unity.com/packages/tools/input-management/easy-save-the-complete-save-load-asset-768)

#### Installation

1. Import [Easy Save](https://assetstore.unity.com/packages/tools/input-management/easy-save-the-complete-save-load-asset-768) package into your project.
2. Define the `UNIDATA_EASYSAVE_SUPPORT` symbol.


### <a id="external-assets-unirx" href="#external-assets-unirx"> UniRx </a>

With the UniRx integration, the following APIs will be available.

- `ReactiveDataCatalog`
- `ReactiveData<T>`
- `ReactiveEntryReference<T>`

These APIs enable powerful event handling with UniData.

UniRx: [https://github.com/neuecc/UniRx](https://github.com/neuecc/UniRx)

#### Installation

1. Import [UniRx](https://github.com/neuecc/UniRx) package into your project.
2. Define the `UNIDATA_UNIRX_SUPPORT` symbol.


## <a id="unidata-pro" href="#unidata-pro"> UniData Pro </a>

This package under development will be available in the AssetStore.

The free version has all the runtime APIs available, but the pro version has a much improved workflow.

A pro version is under development.

![UniDataWindow](https://user-images.githubusercontent.com/13536348/113911425-ebeedd80-9814-11eb-9a8b-1130e746fb59.jpg)


# <a id="author-info" href="#author-info"> Author Info </a>

Hiroya Aramaki is a indie game developer in Japan.

- Blog: [https://mackysoft.net/blog](https://mackysoft.net/blog)
- Twitter: [https://twitter.com/makihiro_dev](https://twitter.com/makihiro_dev)

# <a id="license" href="#license"> License </a>

This library is under the MIT License.