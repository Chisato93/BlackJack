using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<GameObject> cameras;

    private void Start()
    {
        SetCamera((int)CamerasNumber.CAM_NORMAL);
    }

    public void SetCamera(int cameraNumber)
    {
        foreach (var cam in cameras)
            cam.SetActive(false);

        cameras[cameraNumber].SetActive(true);
    }
}
git -c diff.mnemonicprefix=false -c core.quotepath=false --no-optional-locks fetch --prune --tags origin
From https://github.com/Chisato93/Editor_fin
 - [deleted]         (none)     -> origin/Seperate/UI/CopyBookCode
 - [deleted]         (none)     -> origin/Seperate/UI/OriginCode
 - [deleted]         (none)     -> origin/Seperate/UI/UI
 - [deleted]         (none)     -> origin/Widget/ListWidget



 + 5855ecd...5e22e38 main       -> origin/main  (forced update)

git -c diff.mnemonicprefix=false -c core.quotepath=false --no-optional-locks pull origin main
From https://github.com/Chisato93/Editor_fin
 * branch            main       -> FETCH_HEAD

hint: You have divergent branches and need to specify how to reconcile them.
hint: You can do so by running one of the following commands sometime before
hint: your next pull:
hint:
hint:   git config pull.rebase false  # merge
hint:   git config pull.rebase true   # rebase
hint:   git config pull.ff only       # fast-forward only
hint:
hint: You can replace "git config" with "git config --global" to set a default
hint: preference for all repositories. You can also pass --rebase, --no-rebase,

hint: or --ff-only on the command line to override the configured default per
hint: invocation.
fatal: Need to specify how to reconcile divergent branches.







Completed with errors, see above.



    git -c diff.mnemonicprefix=false -c core.quotepath=false --no-optional-locks push -v origin Seperate/UI/CopyBookCode:Seperate/UI/CopyBookCode main:main
POST git-receive-pack (chunked)
error: RPC failed; HTTP 400 curl 22 The requested URL returned error: 400
send-pack: unexpected disconnect while reading sideband packet
fatal: the remote end hung up unexpectedly

Pushing to https://github.com/Chisato93/Editor_fin.git
To https://github.com/Chisato93/Editor_fin.git
 ! [rejected]        main -> main (non-fast-forward)
error: failed to push some refs to 'https://github.com/Chisato93/Editor_fin.git'



hint: Updates were rejected because the tip of your current branch is behind
hint: its remote counterpart. If you want to integrate the remote changes,
hint: use 'git pull' before pushing again.
hint: See the 'Note about fast-forwards' in 'git push --help' for details.



Completed with errors, see above.
