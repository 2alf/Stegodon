# Stegodon
A C# steganography library with encryption and decryption for the .NET Framework.



![icon](https://github.com/2alf/Stegodon/assets/113948114/19a39ef0-98a8-477c-8657-8dc4412f13fa)


### Made from scratch for the purpose of building the Leonardo Desktop App.

https://www.nuget.org/packages/Stegodon

# README IS WIP


## Usage


### Encrypt

Encrypt() takes 2 arguments. First is `Bitmap` and the seocond one is a `string`.
```
// Example
string imagePath = "path/to/your/img.jpg"
string secretMsg = "p@55w0rd"

Bitmap encryptedImage = Steganography.Encrypt(new Bitmap(imagePath), secretMsg);
```

Output is a prompt to save your image as a `.png` file. 

### Decrypt

Decrypt() takes a Bitmap argument.
In the below example we assume that the user wants to decrypt a string from an image. 
```
// Example
string imagePath = openFileDialog.FileName;
Bitmap chosenImg = new Bitmap(imagePath);

Steganography.Decrypt(chosenImg);
```

Output is the decrypted string.
