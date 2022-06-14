package main

import (
	"crypto/hmac"
	"crypto/sha256"
	"encoding/hex"
	"fmt"
	"log"
)

func verifySignature(secret string, payload string, signature string) (bool, error) {
	mac := hmac.New(sha256.New, []byte(secret))
	mac.Write([]byte(payload))
	expectedMac := mac.Sum(nil)
	actualMac, err := hex.DecodeString(signature)
	if err != nil {
		return false, err
	}
	return hmac.Equal(actualMac, expectedMac), nil
}

func main() {
	secret := "test-secret"
	payload := `{"monday":"75F","tuesday":"80F"}`
	signature := "b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151"

	validity, err := verifySignature(secret, payload, signature)
	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(fmt.Sprintf("Is the signature valid? %t", validity))
}
