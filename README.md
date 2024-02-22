# README IS WIP
# Stegodon

Quick and easy C# steganography library for encrypting and decrypting strings into image files when building .NET Framework apps.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/C1C3UABOS)
![NuGet Downloads](https://img.shields.io/nuget/dt/Stegodon?style=for-the-badge&logo=c%23&logoColor=%2350FF00&label=INSTALLS&color=%233e9544)



<img src="https://github.com/2alf/Stegodon/assets/113948114/2706e52d-e42b-4eb9-a3a6-54157230de66" width="250"/>



### Made from scratch for the purpose of building the [Leonardo Desktop App](https://github.com/2alf/Leonardo).

## Contents:

>[Download](#download)

>[Usage](#usage)

>[Further info](#further-info)

## Download

### DLL Binary

>[Github](https://github.com/2alf/Stegodon/releases)

>[NuGet](https://www.nuget.org/packages/Stegodon)

### .NET Cli

```bash
dotnet add package Stegodon
```

### Visual studio:

Right click on your project solution and go to your NuGet package manager.


<img src="https://github.com/2alf/Stegodon/assets/113948114/45de3351-2bfd-410a-8925-fc60526538e9" width="550"/>


<img src="https://github.com/2alf/Stegodon/assets/113948114/5ee88f00-b6d2-4921-a9b8-93c425e4c8c9" width="550"/>


## Usage

>[Encrypt](#encrypt)

>[Decrypt](#decrypt)

### Encrypt

Encrypt() takes 2 arguments. First is `Bitmap` and the seocond one is a `string`.
```cs
// Example
string imagePath = "path/to/your/img.jpg"
string secretMsg = "p@55w0rd"

Bitmap encryptedImage = Stegodon.Encrypt(new Bitmap(imagePath), secretMsg);
```

Output is a prompt to save your image as a `.png` file. 

### Decrypt

Decrypt() takes a Bitmap argument.
In the below example we assume that the user wants to decrypt a string from an image. 
```cs
// Example
string imagePath = openFileDialog.FileName;
Bitmap chosenImg = new Bitmap(imagePath);

Stegodon.Decrypt(chosenImg);
```

Output is the decrypted string.

## Further info
The solution I made isn’t anything new and broad, but it is concentrated in allowing users to use steganography easily to encrypt and decrypt strings into and from image files. The encryption process first normalizes the image making sure all pixel data is even to minimize error whilst injecting new bits into the file and to avoid breaking pixels. Then we just embed string binary data into the least significant bits of each RGB color channel. The decryption is simply extracting back information from the LSB of each channel.  
