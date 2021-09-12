using UnityEngine;

public class CommentableSO : ScriptableObject
{
#if UNITY_EDITOR
    /// <summary>
    /// 오직 유니티 에디터에서만 볼 수 있는 Help 문자열. 이 변수를 런타임에 참조할 수 없으므로 주의
    /// </summary>
    [TextArea(2, 5)] public string HelpComment;
#endif
}
